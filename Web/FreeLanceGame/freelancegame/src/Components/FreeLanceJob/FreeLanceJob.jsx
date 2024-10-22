import React, { Component } from "react";

import { getSkillName } from "../../HelperMethods/common";
import _ from "lodash";
import { JobItem } from "../Common/JobRelated/JobItem";
import { MyButton } from "../Common/Buttons/Button";
import { JobWrapper } from "../Common/JobRelated/JobWrapper";
import { Colors } from "../Common/Colors";
export default class FreeLanceJob extends Component {
  constructor(props) {
    super(props);
    this.state = {};
  }

  renderJobList(jobsToShow) {
    if (!jobsToShow || (jobsToShow && jobsToShow.length === 0)) return null;
    return jobsToShow.map((job) => {
      if (job.hide) return null;
      return (
        <JobItem
          key={job.id}
          job={job}
          handleJobClick={(e) => {
            this.props.handleJobClick(job, "apply");
          }}
        />
      );
    });
  }
  render() {
    let jobList = this.renderJobList(this.props.jobsToShow);
    return (
      <div>
        <div>
          <MyButton onClick={(e) => this.props.handleMenuItemClick("MenuPage")}>
            Back
          </MyButton>
        </div>
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
         Available Jobs
          </div>
          <div>{jobList}</div>
        </JobWrapper>
      </div>
    );
  }
}
