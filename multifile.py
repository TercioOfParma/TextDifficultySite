import requests
import asyncio
import datetime
import aiohttp

SUBMIT_URL= "http://localhost:5277/CheckText"

REQUEST_LANGUAGE= "296f4d4c-0226-4a6f-99cb-01d606a51c33"
REQUEST_TEXT = """Et cum complerentur dies Pentecostes, erant omnes pariter in eodem loco: et factus est repente de caelo sonus, tamquam advenientis spiritus vehementis, et replevit totam domum ubi erant sedentes. Et apparuerunt illis dispertitae linguae tamquam ignis, seditque supra singulos eorum: et repleti sunt omnes Spiritu Sancto, et coeperunt loqui variis linguis, prout Spiritus Sanctus dabat eloqui illis. Erant autem in Jerusalem habitantes Judaei, viri religiosi ex omni natione quae sub caelo est. Facta autem hac voce, convenit multitudo, et mente confusa est, quoniam audiebat unusquisque lingua sua illos loquentes. Stupebant autem omnes, et mirabantur, dicentes: Nonne ecce omnes isti qui loquuntur, Galilaei sunt? et quomodo nos audivimus unusquisque linguam nostram in qua nati sumus? Parthi, et Medi, et  Aelamitae, et qui habitant Mespotamiam, Judaeam, et Cappadociam, Pontum, et Asiam, Phrygiam, et Pamphyliam,  Aegyptum, et partes Libyae quae est circa Cyrenen: et advenae Romani, Judaei quoque, et Proselyti, Cretes, et Arabes: audivimus eos loquentes nostris linguis magnalia Dei. Stupebant autem omnes, et mirabantur ad invicem, dicentes: Quidnam vult hoc esse? Alii autem irridentes dicebant: Quia musto pleni sunt isti.  Stans autem Petrus cum undecim, levavit vocem suam, et locutus est eis: Viri Judaei, et qui habitatis Jerusalem universi, hoc vobis notum sit, et auribus percipite verba mea. Non enim, sicut vos aestimatis, hi ebrii sunt, cum sit hora diei tertia: sed hoc est quod dictum est per prophetam Joel: [Et erit in novissimis diebus, dicit Dominus, effundam de Spiritu meo super omnem carnem: et prophetabunt filii vestri et filiae vestrae, et juvenes vestri visiones videbunt, et seniores vestri somnia somniabunt. Et quidem super servos meos, et super ancillas meas, in diebus illis effundam de Spiritu meo, et prophetabunt: et dabo prodigia in caelo sursum, et signa in terra deorsum, sanguinem, et ignem, et vaporem fumi: sol convertetur in tenebras, et luna in sanguinem, antequam veniat dies Domini magnus et manifestus. Et erit: omnis quicumque invocaverit nomen Domini, salvus erit.] Viri Israelitae, audite verba haec: Jesum Nazarenum, virum approbatum a Deo in vobis, virtutibus, et prodigiis, et signis, quae fecit Deus per illum in medio vestri, sicut et vos scitis: hunc, definito consilio et praescientia Dei traditum, per manus iniquorum affligentes interemistis: quem Deus suscitavit, solutis doloribus inferni, juxta quod impossibile erat teneri illum ab eo. David enim dicit in eum: [Providebam Dominum in conspectu meo semper: quoniam a dextris est mihi, ne commovear: propter hoc laetatum est cor meum, et exsultavit lingua mea, insuper et caro mea requiescet in spe: quoniam non derelinques animam meam in inferno, nec dabis sanctum tuum videre corruptionem. Notas mihi fecisti vias vitae: et replebis me jucunditate cum facie tua.] Viri fratres, liceat audenter dicere ad vos de patriarcha David, quoniam defunctus est, et sepultus: et sepulchrum ejus est apud nos usque in hodiernum diem. Propheta igitur cum esset, et sciret quia jurejurando jurasset illi Deus de fructu lumbi ejus sedere super sedem ejus: providens locutus est de resurrectione Christi, quia neque derelictus est in inferno, neque caro ejus vidit corruptionem. Hunc Jesum resuscitavit Deus, cujus omnes nos testes sumus. Dextera igitur Dei exaltatus, et promissione Spiritus Sancti accepta a Patre, effudit hunc, quem vos videtis et auditis. Non enim David ascendit in caelum: dixit autem ipse: [Dixit Dominus Domino meo: Sede a dextris meis, donec ponam inimicos tuos scabellum pedum tuorum.] Certissime sciat ergo omnis domus Israel, quia et Dominum eum et Christum fecit Deus hunc Jesum, quem vos crucifixistis.  His autem auditis, compuncti sunt corde, et dixerunt ad Petrum et ad reliquos Apostolos: Quid faciemus, viri fratres? Petrus vero ad illos: Poenitentiam, inquit, agite, et baptizetur unusquisque vestrum in nomine Jesu Christi in remissionem peccatorum vestrorum: et accipietis donum Spiritus Sancti. Vobis enim est repromissio, et filiis vestris, et omnibus qui longe sunt, quoscumque advocaverit Dominus Deus noster. Aliis etiam verbis plurimis testificatus est, et exhortabatur eos, dicens: Salvamini a generatione ista prava. Qui ergo receperunt sermonem ejus, baptizati sunt: et appositae sunt in die illa animae circiter tria millia.  Erant autem perseverantes in doctrina Apostolorum, et communicatione fractionis panis, et orationibus. Fiebat autem omni animae timor: multa quoque prodigia et signa per Apostolos in Jerusalem fiebant, et metus erat magnus in universis. Omnes etiam qui credebant, erant pariter, et habebant omnia communia. Possessiones et substantias vendebant, et dividebant illa omnibus, prout cuique opus erat. Quotidie quoque perdurantes unanimiter in templo, et frangentes circa domos panem, sumebant cibum cum exsultatione et simplicitate cordis, collaudantes Deum et habentes gratiam ad omnem plebem. Dominus autem augebat qui salvi fierent quotidie in idipsum."""


MY_POST = {'Text' : REQUEST_TEXT, 'LanguageId' : REQUEST_LANGUAGE}

async def send_request(number):
    print(f"Sent {number}")
    res = requests.post(SUBMIT_URL, json=MY_POST)
    print(f"Done! {number}")
    return res
async def doing(tasks):
    await asyncio.gather(*tasks)

tasks = []
for i in range(1000):
    tasks.append(send_request(i))
begin = datetime.datetime.now()
loop = asyncio.get_event_loop()
loop.run_until_complete(doing(tasks))
end = datetime.datetime.now()
loop.close()
print(end - begin)