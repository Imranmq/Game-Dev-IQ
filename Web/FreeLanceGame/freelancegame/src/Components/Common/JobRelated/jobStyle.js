import { Colors } from "../Colors";
export const jobItemStyle = {
  rowStyle: {
    marginBottom: "15px",
  },

  container: {
    fontSize: "15px",
    border: "3px solid",
    borderRadius: "5px",
    borderColor: Colors.thirdWheel,
    background: Colors.major,
    color: Colors.majorText,
    padding: "15px",
    marginBottom: "15px",
  },
  fieldRow: { textAlign: "left" },
  detailRowContainer: {
    textAlign: "left",
    border: "3px solid",
    borderColor: Colors.thirdWheel,
    background: Colors.minor,
    borderRadius: "5px",
    padding: "5px",
  },
  detailLabelStyle: {
    fontWeight: 700,
  },
  detailArea: {
    marginTop: "15px",
    fontSize: "14px",
    textAlign: "left",
    minHeight: "100px",
  },
  skillContainer: { textAlign: "left" },
  skillsLabel: { fontWeight: 700 },
  skillRow: { textAlign: "left", marginTop: "5px" },
  progressBarContainer: {
    background: "black",
    minWidth: "200px",
    height: "25px",
    display: "inline-block",
  },
  progressBar: { background: "green", height: "inherit" },
};
export const jobWrapperStyle = {
  jobWrapperStyle: {
    padding: "15px",
    background: Colors.fourthWheel,
    minHeight: window.innerHeight - 80 + "px",
    border: "4px solid",
    borderRadius: "10px",
    borderColor: Colors.thirdWheel,
  },
};
