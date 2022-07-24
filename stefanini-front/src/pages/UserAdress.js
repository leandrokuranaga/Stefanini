import axios from "axios";
import { useState, useEffect } from "react";

const UserAddress = () => {
  const [users, setUsers] = useState();

  axios.get("localhost");

  useEffect(() => {}, []);

  return (
    <div>
      <p>Olá</p>
    </div>
  );
};

export default UserAddress;
