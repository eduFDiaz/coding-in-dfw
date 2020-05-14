import { Tag } from './Tag';

export interface Post {
    id: string,
    title: string,
    description: string,
    text: string,
    createdat: Date,
    publishedat: Date,
    tags?: Tag[]
    readingtime: number,
    userid: number
}
