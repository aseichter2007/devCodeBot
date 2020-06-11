function AxiosPostRequest(output){
    
    const axios = require("axios");
 axios.axios.post("https://localhost:44317/api/values/" , output).then((response) => {
     return response;//not sure if this is gonna work like I want in javascript. 
    console.log(response);
  }, (error) => {
    console.log(error);
  });
}
exports.AxiosPostRequest=AxiosPostRequest