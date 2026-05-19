/*
 * Problem -1: User Profile Data Handling
 */

const userName: string = "Roshan";
const email: string = "roshan@gmail.com";
let age: number = 24;
let isSubscribed: boolean = true;

//formatted user profile message 
let profileMessage = `Hello ${userName}, you are ${age} years old and your email is ${email}.`;
console.log(profileMessage);

// Type Inference 
let accountStatus = "Active"; 
const maxAttempts = 3; 

// Operators: Logical and Arithmetic

age++; 

// Check if user is eligible for a premium plan (age > 18 AND subscribed)
const isEligibleForPremium: boolean = age > 18 && isSubscribed;

// Template Literals and Output
console.log("--- User Profile Information ---");


console.log(`Account Status: ${accountStatus}`);
console.log(`Eligible for Premium Plan: ${isEligibleForPremium ? "Yes" : "No"}`);

// comparison operators
if (age >= 18) {
    console.log(`${userName} is an adult.`);
} else {
    console.log(`${userName} is a minor.`);
}
