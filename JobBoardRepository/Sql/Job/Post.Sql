UPDATE Job
    SET ProductId= @ProductId
        ,Description=@Description
        ,MaxPay=@MaxPay
        ,MinPay=@MinPay
        ,SubTitle=@SubTitle
        ,Title = @Title
WHERE ProductId = @ProductId