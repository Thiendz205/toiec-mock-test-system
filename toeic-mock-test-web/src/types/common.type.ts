export const RecordStatus = {
  Inactive: 0,
  Active: 1,
  Suspended: 2,
  Delete: 3,
} as const;

export type RecordStatusType = (typeof RecordStatus)[keyof typeof RecordStatus];