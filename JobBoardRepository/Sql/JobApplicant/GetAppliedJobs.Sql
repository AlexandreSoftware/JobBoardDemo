SELECT * FROM Job JOIN JobApplicant JA on Job.ProductId = JA.Job
WHERE @Id = JA.Applicant;