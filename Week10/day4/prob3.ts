class Employee{
    public id:number;
    protected __name:string;
    private _salary:number;

    constructor(id:number,__name:string,_salary:number)
    {
        this.id=id;
        this.__name=__name;
        this._salary=_salary;
    }
   
    // Getter for salary
    get salary(): number {
        return this._salary;
    }
      // Setter for salary with validation
    set salary(value: number) {
        if (value > 0) {
            this._salary = value;
        } else {
            console.log("Error: Salary must be a positive number.");
        }
    }
    displayDetails():void{
        console.log(`Id=${this.id}, Name:${this.__name}, Salary:${this._salary}`)
    }
}

class Manager extends Employee{
    public teamsize:number;

    constructor(id: number, __name: string, _salary: number,teamsize:number)
    {
          // Pass the arguments to the parent class constructor
         super(id, __name, _salary); 
        this.teamsize=teamsize;
    }
    displayDetails():void{
        console.log(`Id=${this.id} Name:${this.__name} Salary:${this.salary}  teamsize:${this.teamsize} `)
    }
}
// --- Object Creation ---

// 1. Employee Object
console.log("--- Employee Details ---");
const emp = new Employee(101, "Roshan", 50000);
emp.displayDetails();


// 2. Manager Object
console.log("\n--- Manager Details ---");
const mgr = new Manager(201, "Aj", 85000, 12);
mgr.displayDetails();
