# dotnet aspnet-codegenerator controller \
#  -name AgendamentoController           \
#  -async -api                           \
#  -m Agendamento                        \
#  -dc AppDbContext                      \
#  -outDir Controllers

dotnet aspnet-codegenerator controller \
 -name ServicoController               \
 -async -api                           \
 -m Servico                            \
 -dc AppDbContext                      \
 -outDir Controllers