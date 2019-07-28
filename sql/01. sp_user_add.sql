use Cryptozon
go

drop procedure if exists sp_user_add
go

create procedure sp_user_add
     ( @firstName     nvarchar(250),
       @surname       nvarchar(250),
       @username      nvarchar(100),
       @passwordSalt  nvarchar(200),
       @passwordHash  nvarchar(100) )
as
begin
  begin try
    insert dbo.Users
         ( FirstName,
           Surname,
           Username,
           PasswordSalt,
           PasswordHash,
           LastEditedOn )
    select @firstName,
           @surname,
           @username,
           @passwordSalt,
           @passwordHash,
           getdate()
  end try
  begin catch
    declare @error_number  int           = error_number(),
            @error_message nvarchar(max) = concat(error_message(), ' [sp_user_add]'),
            @error_state   int           = error_state();

    throw @error_number, @error_message, @error_state
  end catch
end
go

grant execute on sp_user_add to public
go