function AxiosPostRequest(input){
    var output;
 axios.axios.post("https://localhost:44317/api/values/" , output).then((response) => {
      output= response;
      console.log(response);
    }, (error) => {
      output = error;
      console.log(error);
    });
  return output;// I think this should let me get my response back to where I call it. maybe. 
}
exports.AxiosPostRequest=AxiosPostRequest