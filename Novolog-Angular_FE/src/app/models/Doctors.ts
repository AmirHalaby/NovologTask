import { languages } from "./languages";
export class Doctors  {
    constructor(
        public fullName: String,
        public phoneName: number,
        public languagesId: languages[],
        public serviceRate: number,
    ){}
}