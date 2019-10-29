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
  photos: string;
  posts: string;
  username: string;
}
