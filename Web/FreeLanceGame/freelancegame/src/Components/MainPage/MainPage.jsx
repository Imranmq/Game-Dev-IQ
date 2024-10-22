import React, { Component } from "react";
import Dashboard from "./Dashboard";
import FreeLanceJob from "../FreeLanceJob/FreeLanceJob";
import TopInfoBar from "../TopInfoBar/TopInfoBar";
import _ from "lodash";
import { jobs } from "../FreeLanceJob/Jobs";

export default class MainPage extends Component {
  constructor(props) {
    super(props);
    this.state = {
      renderPage: "MenuPage",
      money: 0,
      day: 1,
      hour: 0,
      nextHourSize: 2,

      activeJobs: [],

      todaysJobs: [
        { ...jobs[0] },
        { ...jobs[0], id: "A2" },
        { ...jobs[0], id: "A3" },
      ],
    };
    this.expiredJobs = [];
    this.handleMenuItemClick = this.handleMenuItemClick.bind(this);
    this.handleJobClick = this.handleJobClick.bind(this);
    this.handleAppliedJob = this.handleAppliedJob.bind(this);
    this.handleWorkJob = this.handleWorkJob.bind(this);
  }
  handleAppliedJob(job) {
    let jobsToShow = [...this.state.todaysJobs];
    _.remove(jobsToShow, { id: job.id });
    this.setState({ todaysJobs: jobsToShow });
  }
  handleJobClick(job, method) {
    let activeJobsList = [...this.state.activeJobs];
    if (method === "apply") {
      if (isValidJob(job)) {
        this.handleAppliedJob(job);
        let aJob = _.cloneDeep(job);

        aJob.active = true;
        aJob.timeLeft = job.hours;
        activeJobsList.push(aJob);
      }
    } else if (method === "cancel") {
      _.remove(activeJobsList, { id: job.id });
    }
    this.setState({
      activeJobs: activeJobsList,
    });
  }
  handleWorkJob(job) {
    let activeJobs = [...this.state.activeJobs];
    let stateToUpdate = {};
    let jObj = _.find(activeJobs, { id: job.id });
    if (jObj) {
      let progress = 20;
      !jObj.completion && (jObj.completion = 0);
      jObj.completion = jObj.completion + progress;
      if (jObj.completion >= 100) {
        stateToUpdate.money = this.completedJob(job);
        _.remove(activeJobs, { id: job.id });
      }
    }

    stateToUpdate = { ...stateToUpdate, ...this.tickTime() };
    let activeAndExpiredJobs = this.tickTimeJobsRemaining(activeJobs);
    stateToUpdate.activeJobs = activeAndExpiredJobs.activeJobs;
    this.expiredJobs = [...this.expiredJobs, activeAndExpiredJobs.expiredJobs];
    this.setState({
      ...stateToUpdate,
    });
  }
  completedJob(job) {
    let money = this.state.money;
    money = money + job.payment;
    return money;
  }
  tickTime() {
    const { nextHourSize, hour, day } = this.state;
    let newHour = hour + nextHourSize;
    let newDay = day;
    if (newHour < 48) {
      if (newHour >= 24) {
        let remainder = newHour % 24;
        newDay++;
        newHour = remainder;
      }
      return { day: newDay, hour: newHour };
    }
    return {};
  }

  tickTimeJobsRemaining(activeJobs) {
    let activeAndExpired = {
      activeJobs: activeJobs,
      expiredJobs: [],
    };
    let aJobLen = activeJobs.length;

    for (let i = 0; i < aJobLen; i++) {
      activeJobs[i].timeLeft = activeJobs[i].timeLeft - this.state.nextHourSize;
      if (activeJobs[i].timeLeft <= 0) {
        activeAndExpired.expiredJobs.push(activeJobs[i]);
      }
    }
    _.remove(activeAndExpired.activeJobs, (item) => {
      return _.find(activeAndExpired.expiredJobs, { id: item.id });
    });
    return activeAndExpired;
  }

  handleMenuItemClick(item) {
    this.setState({
      renderPage: item,
    });
  }
  render() {
    let propsToPass = {
      menuProps: {
        handleMenuItemClick: this.handleMenuItemClick,
        handleJobClick: this.handleJobClick,
        handleWorkJob: this.handleWorkJob,
        activeJobs: this.state.activeJobs,
      },
      freeLanceJobProps: {
        handleJobClick: this.handleJobClick,
        handleMenuItemClick: this.handleMenuItemClick,
        today: this.state.day,
        jobsToShow: this.state.todaysJobs,
      },
    };
    let toRender = renderPages(propsToPass)[this.state.renderPage];
    toRender = toRender ? toRender : null;
    return (
      <div>
        <TopInfoBar
          day={this.state.day}
          money={this.state.money}
          hour={this.state.hour}
        />
        {toRender}
      </div>
    );
  }
}

const renderPages = (props) => {
  return {
    MenuPage: <Dashboard {...props.menuProps} />,
    FreeLanceJob: <FreeLanceJob {...props.freeLanceJobProps} />,
  };
};

function isValidJob(job) {
  return true;
}
