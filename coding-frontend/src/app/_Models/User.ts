import { Photo } from "./Photo";
import { Posts } from "./Posts";

export interface User {
  created: Date;
  email: string;
  fullName: string;
  id: number;
  lastActive: Date;
  passwordHash: string;
  passwordSalt: string;
  /* photos: Photo[]; */
  /* posts: Post[]; */
  photos: Photo[];
  posts: Posts[];
  username: string;
}
