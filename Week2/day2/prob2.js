const calculateTotal=(cart)=>
  cart.reduce((total,item)=>total+item.price*item.quantity,0);

const formatInvoice=(cart) =>
  cart
    .map((item)=>`${item.name}:$${item.price}x${item.quantity}`)
    .join("\n");

const cart=[
  {name:"Laptop",price:10000,quantity:1},
  {name:"Mouse",price:2500,quantity:2},
];

const total=calculateTotal(cart);
const items=formatInvoice(cart);
const invoiceOutput= `--- Invoice ---
${items}
---------------
Total:$${total}`;

document.getElementById("d1").innerText = invoiceOutput;
console.log(invoiceOutput);
