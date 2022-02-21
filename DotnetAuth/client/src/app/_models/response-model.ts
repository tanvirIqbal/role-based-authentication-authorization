import { ResponseCode } from "./enums";

export class ResponseModel {
    code: ResponseCode = ResponseCode.NotSet;
    message: string = '';
    dataSet: any;

    // constructor(code: ResponseCode, message: string) {
    //     this.message = message;
    //     this.code = code;
    // }
}