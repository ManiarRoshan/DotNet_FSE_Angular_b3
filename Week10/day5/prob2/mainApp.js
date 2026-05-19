"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const constants_1 = require("./constants");
const Utility_1 = require("./Utility");
const Service_1 = require("./Service");
const Students = [
    { id: 1, name: "Roshan", marks: 98 },
    { id: 2, name: "Jeevan", marks: 88 },
    { id: 3, name: "suri", marks: 78 }
];
Students.forEach(s => {
    const status = s.marks >= constants_1.PASS_MARKS ? "PASS" : "FAIL";
    console.log(`${(0, Utility_1.formatName)(s.name)}-Grade: ${(0, Service_1.getGrade)(s.marks)} --Status:${status}`);
});
console.log(`\nAverage Marks: ${(0, Utility_1.calculateAverage)(Students).toFixed(2)}`);
const topper = (0, Service_1.getTopper)(Students);
console.log(`Topper: ${(0, Utility_1.formatName)(topper.name)} (${topper.marks} marks)`);
