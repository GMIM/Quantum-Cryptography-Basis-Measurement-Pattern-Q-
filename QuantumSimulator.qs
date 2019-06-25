namespace Quantum.BasisMeasurementPattern
{
    open Microsoft.Quantum.Intrinsic;
    open Microsoft.Quantum.Canon;
	 operation measureInZBasis(qubit : Qubit):Result{
	              
				 let result =  Measure([PauliZ], [qubit]);
				 return result;
	   }
	 
	 operation measureInXBasis(qubit : Qubit):Result{
	              
				 let result =  Measure([PauliX], [qubit]);
				 return result;
	   }

	 operation Teleport (msg : Qubit, target : Qubit) : Unit {
        using (register = Qubit()) {
            // Create some entanglement that we can use to send our message.
            H(register);
            CNOT(register, target);

            // Encode the message into the entangled pair,
            // and measure the qubits to extract the classical data
            // we need to correctly decode the message into the target qubit:
            CNOT(msg, register);
            H(msg);
            let data1 = M(msg);
            let data2 = M(register);

            // decode the message by applying the corrections on
            // the target qubit accordingly:
            if (data1 == One) { Z(target); }
            if (data2 == One) { X(target); }

            // Reset our "register" qubit before releasing it.
            Reset(register);
        }
    }


	operation measureRecieverSide(Q2 : Qubit): Bool{
	     
	     let MeasurementResultZBasis = M(Q2);
		 if( MeasurementResultZBasis==One){
			return true;
		 }
		 else {
			return false;
		 }
	
	}

	operation createBasisMeasurementPatternFromSender() : (Bool[],Bool[]) {
		//sender side
		mutable ALiceBasisMeasurementPattern =  new Bool[8];
		mutable BobLiceBasisMeasurementPattern =  new Bool[8];
		//Using Q1,Q2,Q3
		for(i in 0..7){
		  using(  (Q1,Q2) = (Qubit(), Qubit())  ){
		         //hadmard gate to apply super posiion 
				 H(Q1);
				 //Make entanglement by applying CNOT gate
				 CNOT(Q1,Q2);
				 //send Q2 to be measured
				 Message("Q1 and Q2 are entangled for qubit Now sending qubit to Bob");
				 Message("Q1 and Q2 are entangled for qubit sending qubit to Bob");
				 if(i==0){
				      Message("Ending of iteration pattern for 1-th bit Basis in the BMP");
				 }
				 if(i==1){
				      Message("Ending of iteration pattern for 2-th bit Basis in the BMP");
				 }
				 if(i==2){
				      Message("Ending of iteration pattern for 3-th bit Basis in the BMP");
				 }
				 if(i==3){
				      Message("Ending of iteration pattern for 4-th bit Basis in the BMP");
				 }
				 if(i==4){
				      Message("Ending of iteration pattern for 5-th bit Basis in the BMP");
				 }
				 if(i==5){
				      Message("Ending of iteration pattern for 6-th bit Basis in the BMP");
				 }
				 if(i==6){
				      Message("Ending of iteration pattern for 7-th bit Basis in the BMP");
				 }
				 if(i==7){
				      Message("Ending of iteration pattern for 8-th bit Basis in the BMP");
				 }
				 let BobResultFromQ2 = measureRecieverSide(Q2);
				 set BobLiceBasisMeasurementPattern w/=i<-BobResultFromQ2;
				 let AliceMeasurementQ1 = M(Q1);
				 if(AliceMeasurementQ1==Zero){set ALiceBasisMeasurementPattern w/=i<-false;}
				 else {
				    set ALiceBasisMeasurementPattern w/=i<-true;
				 }
				 
				 Reset(Q1);
				 Reset(Q2);
		   }		
		}

		return (ALiceBasisMeasurementPattern, BobLiceBasisMeasurementPattern);
	}

   operation HelloQ () : Unit {
          using(qubits = Qubit[10]){
		     for(i in 0..8){
			   if( M(qubits[i])==Zero){
			      X(qubits[i]);
				  H(qubits[i]);
				  if(M(qubits[i])==One){
				     //Message("Okay");
				  }
				  else {
				   //Message("Am Done");
				  }
			   }
			  
			 }
			 ResetAll(qubits);

		  
		  }    
	}
}
