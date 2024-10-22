import { Colors } from "../Common/Colors";
import comp from "../../images/Computer 32x32.PNG";

export const Menu = (props) => {
  return <div style={menuWrapperStyle}>{renderMenuItems(props)}</div>;
};

function renderMenuItems(props) {
  let list = MenuItems.map((item) => {
    return (
      <div style={imageContainerStyle}>
        <img
          src={item.src}
          alt={item.alt}
          style={{ ...imageSize, ...imageStyle }}
          onClick={(e) => {
            props.handleMenuItemClick(item.path);
          }}
        />
        <span style={imageTextStyle}>{item.text}</span>
      </div>
    );
  });
  return list;
}

const MenuItems = [
  { src: comp, alt: "jobs", path: "FreeLanceJob", text: "Freelance" },
];
const menuWrapperStyle = {
  padding: "30px",
  background: Colors.fourthWheel,
  minHeight: window.innerHeight - 80 + "px",
  border: "4px solid",
  borderRadius: "10px",
  borderColor: Colors.thirdWheel,
  textAlign: "left",
};
const imageSize = {
  width: "128px",
  height: "128px",
};

const imageStyle = {
  padding: "16px",
};
const imageContainerStyle = {
  width: "128px",
  border: "2px solid",
  borderColor: Colors.major,
  background: Colors.fourthWheel,
  color: Colors.major,
  cursor: "pointer",
  textAlign: "center",
  paddingBottom: "10px",
};

const imageTextStyle = {
  fontSize: "12px",
  fontWeight: 700,
  padding: "15px",
};
