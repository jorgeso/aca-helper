// require csvtojson
const csv = require("csvtojson");
const converter=csv({
	noheader:true,
	trim:true,
})

// Convert a csv file with csvtojson
csv()
.fromFile("C:/Users/Jorge/Downloads/79467c0c-6079-45cc-b70b-4d1326894d31_logs(1).csv")
.on('json',(jsonObj)=>{
	console.log(jsonObj);
})
.on('done',(error)=>{
	console.log('end')
})