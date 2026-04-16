 import { RecordStatusType } from "@/types/common.type";

    export interface UserResponse {
    id: string;
    name: string;
    email: string;
    firstName: string;
    lastName: string;
    fullName: string; 
    roleId: string;
    roleName: string;
    status: RecordStatusType;
    createdDate: string; 
    }

    export interface UserDetailResponse extends UserResponse {
    createdById?: string;
    updatedDate?: string;
    updatedById?: string;
    }