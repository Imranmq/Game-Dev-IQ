import React, { Component } from "react";
import { Colors } from "../Common/Colors";

export default class TopInfoBar extends Component {
  render() {
    return (
      <div
        style={{
          background: Colors.thirdWheel,
          height: "50px",
          color: Colors.thirdWheelText,
          padding: "5px",
          fontSize: "18px",
        }}
      >
        <MoneyCount money={this.props.money} />{" "}
        <DayCount day={this.props.day} />{" "}
        <DayHourCount hour={this.props.hour} />
      </div>
    );
  }
}

const DayCount = (props) => {
  return <span>Day : {props.day}</span>;
};

const MoneyCount = (props) => {
  return <span> $ {props.money}</span>;
};
const DayHourCount = (props) => {
  return <span>Time : {props.hour}</span>;
};
