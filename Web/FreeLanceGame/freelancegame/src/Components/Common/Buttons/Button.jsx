import { Colors } from "../Colors";
export const MyButton = (props) => {
  return (
    <button
      {...props}
      style={{
        padding: "5px 15px",      
        background: Colors.thirdWheel,
        borderColor: Colors.fourthWheel,
        borderRadius: "10px",
        color: Colors.majorText,
      }}
    >
      {props.children}
    </button>
  );
};
