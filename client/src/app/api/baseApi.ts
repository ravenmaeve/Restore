import { BaseQueryApi, FetchArgs, fetchBaseQuery } from "@reduxjs/toolkit/query";
import { startLoading, stopLoading } from "../layout/uiSlice";

const customBaseQuery = fetchBaseQuery({
    baseUrl: 'https://localhost:7189/api'
})

const sleep = () => new Promise(resolve => setTimeout(resolve,1000));

export const baseQueryWithErrorHandling = async(args:string|FetchArgs, api:BaseQueryApi, ExtraOptions: object)=>{
   //start loading
   api.dispatch(startLoading());
    await sleep();
   const result = await customBaseQuery(args, api,ExtraOptions); 
    //stop loading
    api.dispatch(stopLoading());
    if(result.error){
        const {status,data} = result.error;
        console.log({status, data});
    }

    return result;
}