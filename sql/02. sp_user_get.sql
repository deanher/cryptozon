use Cryptozon
go

drop procedure if exists sp_user_get
go

create procedure sp_user_get
     ( @username  nvarchar(100) )
as
begin
  begin try
    select UserId,
           Username, 
           PasswordSalt,
           PasswordHash
      from Users
     where Username = @username
  end try
  begin catch
    declare @error_number  int           = error_number(),
            @error_message nvarchar(max) = concat(error_message(), ' [sp_user_get]'),
            @error_state   int           = error_state();

    throw @error_number, @error_message, @error_state
  end catch
end
go

grant execute on sp_user_get to public
go