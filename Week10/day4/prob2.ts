/*
 *User Notification System
 * Demonstrating parameters, return types, arrow functions, and lexical 'this'.
 */


// Function with Required Parameters
function getWelcomeMessage(name: string): string {
    return `Welcome to our platform, ${name}!`;
}

// Optional Parameters (age is optional)
function getUserInfo(name: string, age?: number): string {
    if (age) {
        return `User: ${name}, Age: ${age}`;
    }
    return `User: ${name}`;
}

// Default Parameters (isSubscribed defaults to false)
const getSubscriptionStatus = (name: string, isSubscribed: boolean = false): string => {
    const status = isSubscribed ? "is currently subscribed" : "is not subscribed";
    return `${name} ${status} to our newsletter.`;
};

// Function with Boolean Return Type
function isEligibleForPremium (age: number): boolean{
    return age >= 18;
};

//Arrow Functions and Lexical 'this'
const NotificationService = {
    appName: "QuickNotify",
    
    // Method using an arrow function to preserve lexical 'this'
    sendAlert: function(userName: string): string {
        // Arrow function inside a method captures 'this' from the surrounding context
        const formatMessage = (msg: string): string => {
            return `[${this.appName}] Notification: ${msg}`;
        };
        
        return formatMessage(`Hey ${userName}, you have a new update!`);
    }
};

// 7. Execution and Output
console.log("--- Notification System Output ---");

// Test Required Parameters
console.log(getWelcomeMessage("jeev"));

// Test Optional Parameters
console.log(getUserInfo("alien", 30));
console.log(getUserInfo("tom")); // Age omitted

// Test Default Parameters
console.log(getSubscriptionStatus("abhi", true));
console.log(getSubscriptionStatus("ellis")); // Uses default false

// Test Boolean Return Type
const userAge = 20;
console.log(`Is user eligible for premium? ${isEligibleForPremium(userAge)}`);

// Test Lexical 'this'
console.log(NotificationService.sendAlert("James"));
