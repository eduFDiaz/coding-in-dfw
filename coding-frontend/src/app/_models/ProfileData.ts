import { Education } from './Education';
import { Language } from './Language';
import { Skill } from './Skill';
import { Award } from './Award';
import { WorkExperience } from './WorkExperience';
import { Project } from './Project';

export interface ProfileData {
    languages: Language[],
    educations: Education[],
    skills: Skill[],
    awards: Award[],
    workExperiences: WorkExperience,
    projects: Project[]

}
