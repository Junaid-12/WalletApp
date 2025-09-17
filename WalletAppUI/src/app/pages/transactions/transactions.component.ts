import { Component, OnInit, Type } from '@angular/core';
import { AuthServiceService } from 'src/app/services/auth.service.service';

@Component({
  selector: 'app-transactions',
  templateUrl: './transactions.component.html',
  styleUrls: ['./transactions.component.css']
})
export class TransactionsComponent implements OnInit {
   
  transactiondata : any [] =[];
  id!:number | null;
  filteredTransactions: any[] = [];
  filterType: string = 'All'; 
  constructor( private authservice : AuthServiceService){ }
    
  
  ngOnInit(): void {
     this.id=this.authservice.getuserId();
     this.authservice.transationHistory(this.id!).subscribe({

      next : (res)=>{
        
          this.transactiondata=res;
          this.applyFilter();
      }});

    
  }
  
  applyFilter() {
    if (this.filterType === 'All') {
      this.filteredTransactions = this.transactiondata;
    } else {
      this.filteredTransactions = this.transactiondata.filter(
        t => t.transactionType === this.filterType
      );
    }
  }

  setFilter(type: string) {
    this.filterType = type;
    this.applyFilter();
  }
}
