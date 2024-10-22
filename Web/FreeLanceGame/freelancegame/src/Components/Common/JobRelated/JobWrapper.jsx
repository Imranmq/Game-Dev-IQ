import { jobWrapperStyle } from "./jobStyle";

export const JobWrapper = (props) => {
  return <div style={jobWrapperStyle.jobWrapperStyle}>{props.children}</div>;
};
