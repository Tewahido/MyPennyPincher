import { useEffect, useRef } from "react";

export default function DiagonalLine({ color }) {
  const canvasRef = useRef(null);

  useEffect(() => {
    const canvas = canvasRef.current;

    if (!canvas) return;

    const width = canvas.clientWidth;
    const height = canvas.clientHeight;

    canvas.width = width;
    canvas.height = height;

    const ctx = canvas.getContext("2d");
    ctx.beginPath();
    ctx.moveTo(0, height);
    ctx.lineTo(width, 0);
    ctx.strokeStyle = color;
    ctx.lineWidth = 2;
    ctx.stroke();
  }, []);

  return <canvas ref={canvasRef} className="w-full h-full bg-transparent " />;
}
