import { useNavigate, useLocation } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { logoutUser } from "../utils/authUtils.js";
import { useEffect } from "react";

export function useTokenChecker(interval = 60000) {
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const location = useLocation();

  const tokenExpiryTime = useSelector((state) => state.user.expiresAt);

  const convertedTokenExpiryTime = new Date(tokenExpiryTime);

  useEffect(() => {
    function checkTokenValidity() {
      if (Date.now() >= convertedTokenExpiryTime.getTime()) {
        logoutUser(dispatch, navigate, location);
      }
    }
    checkTokenValidity();

    const tokenCheckInterval = setInterval(checkTokenValidity, interval);

    return () => clearInterval(tokenCheckInterval);
  }, []);
}
