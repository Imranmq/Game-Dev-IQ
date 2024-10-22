import { getSkillName } from "../../../HelperMethods/common";
import React from "react";
import { jobItemStyle } from "./jobStyle";
import { Row, Col } from "reactstrap";
import { MyButton } from "../Buttons/Button";
export const JobItem = (props) => {
  return (
    <div style={jobItemStyle.container}>
      <Row style={jobItemStyle.rowStyle}>
        <Col md={5}>
          <JobFieldRow
            label={""}
            value={props.job.title}
            style={{ ...jobItemStyle.fieldRow, fontWeight: 700 }}
          />
        </Col>
        <Col md={5}>
          {props.job.active && (
            <JobProgress
              job={props.job}
              style={{
                ...jobItemStyle.progressBarContainer,
              }}
            />
          )}
        </Col>
        <Col md={2}>
          {props.job.active && (
            <JobButtons
              handleMethod={props.handleJobClick}
              text={props.job.active ? "Cancel" : "Apply"}
            />
          )}
        </Col>
      </Row>
      <Row style={jobItemStyle.rowStyle}>
        <Col md={10}>
          <JobDetailRow label={"Job Detail"} value={props.job.detail} />
        </Col>
        <Col md={2}>
          <JobDetailSkills
            label={"Skill Required"}
            value={props.job.skillsRequired}
          />
        </Col>
      </Row>
      <Row>
        <Col md={3}>
          <JobFieldRow label={"Time  : "} value={props.job.hours} />
        </Col>
        <Col md={3}>
          {props.job.timeLeft && (
            <JobFieldRow label={"Rem Time : "} value={props.job.timeLeft} />
          )}
        </Col>
        <Col md={4}>
          <JobFieldRow label={"Payment : "} value={props.job.payment} />
        </Col>
        <Col md={2}>
          {!props.job.active && (
            <JobButtons
              handleMethod={props.handleJobClick}
              text={props.job.active ? "Cancel" : "Apply"}
            />
          )}
          {props.handleWorkJob && (
            <JobButtons handleMethod={props.handleWorkJob} text={"Work"} />
          )}
        </Col>
      </Row>
    </div>
  );
};

export const JobFieldRow = (props) => {
  return (
    <div style={props.style ? props.style : jobItemStyle.fieldRow}>
      {props.label}
      <span>{props.value}</span>
    </div>
  );
};

export const JobDetailRow = (props) => {
  return (
    <div style={props.style ? props.style : jobItemStyle.detailRowContainer}>
      <span style={jobItemStyle.detailLabelStyle}>{props.label}</span>
      <div style={jobItemStyle.detailArea}> {props.value}</div>
    </div>
  );
};

export const JobDetailSkills = (props) => {
  let skills = props.value ? props.value : [];
  if (skills.length > 0) {
    let skillsToShow = skills.map((skill) => {
      return (
        <div style={jobItemStyle.skillRow} key={skill.sCode}>
          {getSkillName(skill.sCode)} <span> {skill.req} </span>
        </div>
      );
    });
    return (
      <div style={jobItemStyle.skillContainer}>
        <span style={jobItemStyle.skillsLabel}>Skills </span>
        {skillsToShow}
      </div>
    );
  } else {
    return null;
  }
};

export const JobButtons = (props) => {
  return (
    <MyButton style={props.style} onClick={props.handleMethod}>
      {props.text}
    </MyButton>
  );
};

export const JobProgress = (props) => {
  let completion = props.job.completion ? props.job.completion : 0;
  return (
    <div>
      <span style={{ marginRight: "10px", verticalAlign: "top" }}>Work</span>

      <div
        style={props.style ? props.style : jobItemStyle.progressBarContainer}
      >
        <div
          style={{ ...jobItemStyle.progressBar, width: completion + "%" }}
        ></div>
      </div>
    </div>
  );
};
