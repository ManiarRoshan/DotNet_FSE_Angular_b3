"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.formatName = formatName;
exports.calculateAverage = calculateAverage;
function formatName(name) {
    return name.charAt(0).toUpperCase() + name.slice(1).toLowerCase();
}
function calculateAverage(students) {
    if (students.length === 0)
        return 0;
    const total = students.reduce((sum, s) => sum + s.marks, 0);
    return total / students.length;
}
