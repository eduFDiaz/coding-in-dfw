import { Photo } from './Photo';
import { Post } from './Post';
import { Product } from './Product';

export interface User {
    id: string;
    username: string;
    fullname: string;
    phone: number;
    email: string;
    shortresume: string;
    fullresume: string;
    photos?: Photo[];
    posts?: Post[];
    products?: Product[],
    location: string,
    githubUrl: string,
    twiterProfile: string,
    facebookProfile: string,
    linkedInProfile: string,
    stackOverflowProfile: string,
    redditProfile: string
    codepenProfile: string
}
