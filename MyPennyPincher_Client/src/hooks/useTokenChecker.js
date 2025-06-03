import { useNavigate, useLocation } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { loginUser, logoutUser } from "../utils/authUtils.js";
import { useEffect } from "react";
import { Refresh } from "../services/authService.js";

export function useTokenChecker(interval = 60000) {
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const location = useLocation();
  const user = useSelector((state) => state.user.user);
  const tokenExpiryTime = useSelector((state) => state.user.expiresAt);

  useEffect(() => {
    if (!tokenExpiryTime || !user) {
      return;
    }

    const convertedTokenExpiryTime = new Date(tokenExpiryTime);

    async function checkTokenValidity() {
      if (Date.now() >= convertedTokenExpiryTime.getTime()) {
        try {
          const response = await Refresh(user.userId);

          if (response.status !== 200) {
            await logoutUser(dispatch, navigate, location, user.userId);
            return;
          }

          const userData = await response.json();

          loginUser(dispatch, userData);
        } catch (error) {
          console.error("Token refresh failed:", error);

          await logoutUser(dispatch, navigate, location, user.userId);
        }
      }
    }

    checkTokenValidity();

    const tokenCheckInterval = setInterval(checkTokenValidity, interval);

    return () => clearInterval(tokenCheckInterval);
  }, [tokenExpiryTime, dispatch, navigate, location, interval]);
}
