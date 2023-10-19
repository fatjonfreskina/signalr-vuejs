<template>
  <div class="row">
    <h1> Total users: {{ totalUsers }}</h1>
    <button @click="addOne">Add One</button>
    <p>{{ counterButton }}</p>
  </div>
</template>

<script>
import * as signalR from '@microsoft/signalr';
const connection = new signalR.HubConnectionBuilder()
  .withUrl("https://localhost:7158/chatHub")
  .withAutomaticReconnect()
  .configureLogging(signalR.LogLevel.Information)
  .build();

export default {
  name: 'HelloWorld',

  data() 
  {
    return {
      counterButton : 0, 
      totalUsers:0,
    }
  },

  created()
  {
    this.start();

    connection.on("addOneReceived", (value) => {
      this.counterButton = value;
      console.log(`Received: ${value}`);
    });

    connection.on("updateTotalUsers", (users, pressed) =>{
      this.totalUsers = users;
      this.counterButton = pressed;
      console.log(`Total users: ${users}`)
    })
  },
  mounted() 
  {
    
  },

  methods: 
  {
    async start(){
      try 
      {
        await connection.start();
        console.log("SignalR Connected.");
      } 
      catch (err) 
      {
          console.log(`Signalr connection error: ${err}`);
      }
    },

    addOne(){
      this.counterButton++;
      connection.send("AddOne")
    }
  },
  
  props: {
    msg: String
  }
}

</script>
