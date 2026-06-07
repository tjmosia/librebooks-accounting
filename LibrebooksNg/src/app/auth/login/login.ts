import { Component, inject, OnInit, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { formValidators, IFormFieldProps } from '../../core/forms';
import { WxFormGroup, WxInputGroup, WxButton } from "../../widgets/core";

@Component({
  selector: 'app-login',
  imports: [WxFormGroup, WxInputGroup, WxButton],
  templateUrl: './login.html',
  styleUrl: './login.scss',
})
export class Login implements OnInit {
  email = signal<string>("")
  model = signal<IFormFieldProps<string>>({value: ""})
  router = inject(Router)
  route = inject(ActivatedRoute)

  ngOnInit(): void {
    const _email = this.route.snapshot.queryParamMap.get("email")
    if(_email == null)
    {
      this.router.navigate(['/auth'])
      return
    }
    this.email.set(_email)
  }

  handleSubmit(event: Event){
    event.preventDefault()
  }

  isValidModel(){
    const _model = this.model()

    if(!_model.value){
      _model.error = "Password is required."
    }

    if(!formValidators.isValidPassword(_model.value))
      _model.error = "Password is invalid."

    if(_model.error)
    {
      this.model.set(_model)
      return false
    }
    return true
  }

  getValidationStateClass(){
    if(this.model().error)
      return "error"
    else 
      return null
  }
}
