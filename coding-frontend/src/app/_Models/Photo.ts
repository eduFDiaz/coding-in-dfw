export interface Photo {
  id: number;
  url: string;
  description: string;
  dateAdded: Date;
  isMain: boolean;
  user: number;
  userId: number;
  publicId: number;
}
