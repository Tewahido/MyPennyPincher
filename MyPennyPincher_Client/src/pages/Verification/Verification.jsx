import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import LoadingAnimation from "../../assets/Dashboard_Loading_Animation.json";
import CompletedAnimation from "../../assets/Completed_Tick_Animation.json";
import FailedAnimation from "../../assets/Failed_Animation.json";
import Lottie from "lottie-react";
import { Verify } from "../../apiServices/authService";

export default function Verification() {
  const navigate = useNavigate();
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(false);

  const queryParams = new URLSearchParams(window.location.search);

  const userId = queryParams.get("userId");
  const token = queryParams.get("token");

  useEffect(() => {
    async function VerifyUser() {
      const response = await Verify(userId, token);

      setLoading(false);

      setTimeout(() => {
        if (response.ok) {
          navigate("/login");
        } else {
          setError(true);
        }
      }, 2000);
    }

    VerifyUser();
  }, []);

  return (
    <div className="flex justify-center bg-green-100 w-full h-full">
      <div className="flex flex-col items-center justify-start pt-30">
        {loading ? (
          <>
            <Lottie animationData={LoadingAnimation} />
            <p className="italic text-green-700 text-4xl">Verifying...</p>
          </>
        ) : error ? (
          <>
            <Lottie animationData={FailedAnimation} />
            <p className="italic text-red-700 text-4xl">Verification Failed</p>
          </>
        ) : (
          <>
            <Lottie animationData={CompletedAnimation} />
            <p className="italic text-green-700 text-4xl">
              Verification Completed
            </p>
          </>
        )}
      </div>
    </div>
  );
}
