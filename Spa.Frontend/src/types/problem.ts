type Error = {
  [name: string]: string[];
};

export type Problem = {
  type: string;
  title: string;
  status: number;
  errors: Error;
};
