import { skills } from "../Skills/skills";
import _ from "lodash";
export function getSkillName(sCode) {
  let obj = _.find(skills, { sCode: sCode });
  if (obj) {
    return obj.name;
  }
  return "";
}
