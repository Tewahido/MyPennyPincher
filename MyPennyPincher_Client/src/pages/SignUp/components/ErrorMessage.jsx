export default function ErrorMessage({ text }) {
  return (
    <p className="text-red-400 text-xs mt-1" style={{ whiteSpace: "pre-wrap" }}>
      {text}
    </p>
  );
}
