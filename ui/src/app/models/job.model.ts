import { CustomerModel } from "./customer.model";

export interface JobModel {
  jobId: number;
  customer: CustomerModel;
  engineer: string;
  when: Date;
}
