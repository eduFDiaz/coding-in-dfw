import { Photo } from './Photo';
import { Requirement } from './Requirement';

export interface Product {
    id?: string,
    name: string,
    type: string,
    url: string,
    productPhoto?: Photo
    description: string,
    userId: number,
    industry: string,
    size: number,
    clientName: string,
    requirements: Requirement[],
    text: string,
    projectIntro: string,
    shortResume: string

}
