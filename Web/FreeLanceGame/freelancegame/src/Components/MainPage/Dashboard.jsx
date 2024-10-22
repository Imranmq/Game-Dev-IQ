import React, { Component } from "react";

import ActiveJobs from "./ActiveJobs";
import { Row, Col } from "reactstrap";
import { Menu } from "./MenuItems";
export default class Dashboard extends Component {
  render() {
    return (
      <Row style={{ margin: "0px", padding: "15px 0px" }}>
        <Col md={6}>
          <ActiveJobs
            activeJobs={this.props.activeJobs}
            handleJobClick={this.props.handleJobClick}
            handleWorkJob={this.props.handleWorkJob}
          />
        </Col>
        <Col md={6}>
          <Menu handleMenuItemClick={this.props.handleMenuItemClick} />
        </Col>
      </Row>
    );
  }
}
