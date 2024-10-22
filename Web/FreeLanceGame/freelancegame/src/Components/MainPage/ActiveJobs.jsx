import React, { Component } from "react";
import { JobItem } from "../Common/JobRelated/JobItem";
import { JobWrapper } from "../Common/JobRelated/JobWrapper";
import { Colors } from "../Common/Colors";

export default class ActiveJobs extends Component {
  render() {
    let jobsToShow =
      this.props.activeJobs && this.props.activeJobs.length > 0
        ? this.props.activeJobs.map((job) => {
            return (
              <JobItem
                job={job}
                handleJobClick={(e) => this.props.handleJobClick(job, "cancel")}
                handleWorkJob={(e) => this.props.handleWorkJob(job)}
              />
            );
          })
        : null;
    return (
      <JobWrapper>
        <div
          style={{
            fontWeight: 700,
            fontSize: "18px",
            color: Colors.major,
            textAlign: "left",
            marginBottom: "15px",
          }}
        >
          Active Jobs
        </div>
        <div>{jobsToShow}</div>
      </JobWrapper>
    );
  }
}
