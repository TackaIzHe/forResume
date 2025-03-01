class obj{
    id;
    text;
    constructor(id,text)
    {
        this.id = id;
        this.text = text;
    }
}

document.addEventListener('click',function(e){

    try{
        const stay = document.createAttribute('stay');
        stay.value = 'd';

        function isValidDate(dateString) {
            const regex = /^\d{4}-\d{2}-\d{2}$/;
            return regex.test(dateString);
        }

        function find(element,tag)
        {
            return element.nodeName == tag ? element :
            find(element.parentNode, tag);
        }

        function sortArr(x,y){
            var x1 = parseFloat(x.text);
            var y1 = parseFloat(y.text);
            var xData = isValidDate(x.text);
            var yData = isValidDate(y.text);
        

            if(xData && yData)
            {
                return x.text.localeCompare(y.text)
            }
            else if(!isNaN(x1) && !isNaN(y1))
            {
                return x.text-y.text
            }
            else return x.text.localeCompare(y.text)
        }
        
        var element = find(e.target,"TH");
        var table = find(element,'TABLE');
        var tr = table.querySelectorAll('TR.sortable');
        var th = table.querySelectorAll('TH');

        
        var atrebute = element.getAttribute('stay');
        if(atrebute == null)
        {
            th.forEach(x =>{
                x.removeAttribute('stay');
            })
            element.setAttribute('stay', 'u');
            sort('u');
        }
        else if(atrebute == 'u')
        {
            console.log(52);
            element.setAttribute('stay', 'd');
            sort('d');
        }
        else if(atrebute == 'd'){
            element.setAttribute('stay', 'u');
            sort('u');
        }


        function sort(attribute){

        var thName=[];
        th.forEach(x=>{
            thName.push(x.innerText);
        })
        
        var elementIndex=0;
        elementIndex = thName.indexOf(element.innerText)
        
        var mass =[];

        tr.forEach((x,index)=>{
            var a = x.querySelectorAll('.input');
            for(let i=0;i<a.length;i++){
                if(i==elementIndex){
                    if(i==0)
                    mass.push(new obj(index,a[i].innerText));
                    else mass.push(new obj(index,a[i].value));
                }
            }});

            
        mass.sort((x,y)=>{
            if(attribute =='u')
            {
                return sortArr(x,y);
            }
            else
            {
                return sortArr(y,x);
            }

        });
        mass.forEach((x)=>{
            table.appendChild(tr[x.id]);
        })

    }


    }
    catch{}
})