const CheckButton = document.querySelector('#btnCheck');
const luhnAlgorithm = (ccNumber) => {
    const length = ccNumber.length;
    let count = 0;

    for (let i = length - 1; i >= 0; i--) {
        let currentDigit = parseInt(ccNumber[i]);

        if ((length - i) % 2 === 0) {
            currentDigit *= 2;
            if (currentDigit > 9) {
                currentDigit = currentDigit % 10 + 1;
            }
        }

        count += currentDigit;
    }

    return (count % 10) === 0;
};
const isVisaCard =(ccNumber)=>
    {  
        return (luhnAlgorithm(ccNumber) && ccNumber.length == 16 && ccNumber.startsWith('4') );
    }

const isMasterCard =(ccNumber)=>
    {
       return (luhnAlgorithm(ccNumber) && ccNumber.length == 16 && (ccNumber.startsWith('22')||ccNumber.startsWith('51')||ccNumber.startsWith('52')||ccNumber.startsWith('53')||ccNumber.startsWith('54')||ccNumber.startsWith('55')));
    }
const isAmExcard=(ccNumber) =>
    {
       return (luhnAlgorithm(ccNumber) && ccNumber.length == 15 && (ccNumber.startsWith('37')||ccNumber.startsWith('34')));

    }
const isDiscover=(ccNumber) =>
    {
       return (luhnAlgorithm(ccNumber) && ccNumber.length == 16 && ccNumber.startsWith('6011'));
    
    }
const checkCC = ()=>{
   const elCCNumber =document.getElementById('ccNumber');
   const elCCValidation =document.getElementById('ccValidation');
   let message="";
    if(isVisaCard(elCCNumber.value))
    {
        message = "Valid Visa Card Number";
    }
    else if (isMasterCard(elCCNumber.value))
    {
        message="Valid Mater Card Number";
    }
    else if (isAmExcard(elCCNumber.value))
    {
        message="Valid AmEx Card Number";
    }
    else if (isDiscover(elCCNumber.value))
    {
        message="Valid Discover Card Number";
    }
    else
    {
        message="Invalid Card Number";
    }
    elCCValidation.textContent = message;
    document.getElementById('ccNumber').value="";
    
};

function addNote(ccNumber,ccValidation)
{   const body={
    "title"      : ccNumber,
    "description": ccValidation,
    "isVisible"  : true
};

    fetch('https://localhost:7065/api/Valication',{
       method:'POST',
       body:JSON.stringify(body),
       headers:
       {
        "content-type":"application/json"
       }
    })
    .then(data =>data.json())
    .then(response=>console.log(response));
}

CheckButton.addEventListener('click',function()
{ const inputCC =document.querySelector('#ccNumber');
  const valid = document.querySelector('#ccValidation');
  addNote(inputCC.value,valid.value);
});