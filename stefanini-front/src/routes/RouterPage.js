import { BrowserRouter, Route, Routes } from "react-router-dom";
import Home from "../pages/Home";
import Register from "../pages/Register";
import UserAddress from "../pages/UserAdress";

const RouterPage = () => {
  const baseName = process.env.PUBLIC_URL;

  return (
    <div>
      <BrowserRouter basename={baseName}>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/register" element={<Register />} />
          <Route path="/useraddress" element={<UserAddress />} />
        </Routes>
      </BrowserRouter>
    </div>
  );
};

export default RouterPage;
