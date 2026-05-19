let stu_marks=[78, 85, 92, 88, 76, 95, 89, 91, 82, 87];
let avg_pass=90;


const total=stu_marks.reduce((a,b)=>(a+b));
console.log("Total marks for student:"+total);

const total_avg=total/stu_marks.length
console.log("Total Average  for student:"+total_avg);

const status =total_avg>=avg_pass? "PASSED":"FAILED";


function c1(){
    if (total_avg >=avg_pass){
        alert(`Student Result is ${status}`)
    }else{
        alert(`Student Result is ${status}`)
}
}



