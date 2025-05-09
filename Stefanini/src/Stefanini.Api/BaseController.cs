﻿using Microsoft.AspNetCore.Mvc;
using Stefanini.Application.Common;
using Stefanini.Domain.SeedWork.Notification;
using System.Net;
using static Stefanini.Domain.SeedWork.NotificationModel;

namespace Stefanini.Api
{
    public class BaseController : ControllerBase
    {
        private readonly INotification _notification;

        protected BaseController(INotification notification)
        {
            _notification = notification;
        }

        private bool IsValidOperation() => !_notification.HasNotification;

        protected IActionResult Response<T>(BaseResponse<T> response)
        {
            if (IsValidOperation())
            {
                if (response.Data == null)
                    return NoContent();

                return Ok(response);
            }

            response.Success = false;
            response.Data = default; 
            response.Error = _notification.NotificationModel;

            return response.Error.NotificationType switch
            {
                ENotificationType.BusinessRules => Conflict(response),
                ENotificationType.NotFound => NotFound(response),
                ENotificationType.BadRequestError => BadRequest(response),
                _ => StatusCode((int)HttpStatusCode.InternalServerError, response)
            };
        }

        protected new IActionResult Response<T>(int? id, object response)
        {
            if (IsValidOperation())
            {
                if (id == null)
                    return Ok(new
                    {
                        success = true,
                        data = response
                    });

                return CreatedAtAction("Get", new { id },
                    new
                    {
                        success = true,
                        data = response ?? new object()
                    });
            }

            return BadRequest(new
            {
                success = false,
                error = _notification.NotificationModel
            });
        }

    }
}
