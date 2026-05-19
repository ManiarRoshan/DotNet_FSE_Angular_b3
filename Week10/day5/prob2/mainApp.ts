import {PASS_MARKS} from "./constants";
import {Student } from "./studentModel";
import { formatName, calculateAverage } from "./Utility";
import { getGrade, getTopper } from "./Service";

const Students:Student[]=[
    {id:1,name:"Roshan",marks:98},
    {id:2,name:"Jeevan",marks:88},
    {id:3,name:"suri",marks:78}
]

Students.forEach(s =>{
    const status= s.marks >= PASS_MARKS? "PASS" : "FAIL";
    console.log(`${formatName(s.name)}-Grade: ${getGrade(s.marks)} --Status:${status}`);
})


console.log(`\nAverage Marks: ${calculateAverage(Students).toFixed(2)}`);
const topper = getTopper(Students);
console.log(`Topper: ${formatName(topper.name)} (${topper.marks} marks)`);


