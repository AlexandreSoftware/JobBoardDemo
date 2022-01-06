UPDATE Applicant
SET Id = @Id
  , Name = @Name
  , WageExpectation = @WageExpectation
WHERE Id = @Id;