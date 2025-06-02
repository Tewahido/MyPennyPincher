import { useNavigate, useLocation } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { logoutUser } from "../utils/authUtils.js";
import { useEffect } from "react";

export function useTokenChecker(interval = 60000) {
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const location = useLocation();
  const userId = useSelector((state) => state.user.user.userId);
  const tokenExpiryTime = useSelector((state) => state.user.expiresAt);

  useEffect(() => {
    if (!tokenExpiryTime) {
      return;
    }

    const convertedTokenExpiryTime = new Date(tokenExpiryTime);

    function checkTokenValidity() {
      if (Date.now() >= convertedTokenExpiryTime.getTime()) {
        logoutUser(dispatch, navigate, location, userId);
      }
    }

    checkTokenValidity();

    const tokenCheckInterval = setInterval(checkTokenValidity, interval);

    return () => clearInterval(tokenCheckInterval);
  }, [tokenExpiryTime, dispatch, navigate, location, interval]);
}
