"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.getGrade = getGrade;
exports.getTopper = getTopper;
function getGrade(marks) {
    if (marks >= 90)
        return "A";
    if (marks >= 80)
        return "B";
    if (marks >= 60)
        return "C";
    return "F";
}
function getTopper(students) {
    return students.reduce((prev, current) => (prev.marks > current.marks ? prev : current));
}
