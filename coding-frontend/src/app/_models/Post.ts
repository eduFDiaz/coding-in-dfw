import { Tag } from './Tag';
import { Commentary } from './Comments';

export interface Post {
    id: string,
    title: string,
    description: string,
    text: string,
    createdat: Date,
    publishedat: Date,
    tags?: Tag[]
    comments?: Commentary[]
    readingtime: number,
    userid: number,
    photourl?: string,
    dateCreated?: string,
    readingTime?: string
}
